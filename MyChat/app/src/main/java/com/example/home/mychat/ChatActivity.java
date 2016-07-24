package com.example.home.mychat;

import android.content.Intent;
import android.database.DataSetObserver;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.*;

import java.lang.reflect.Field;
import java.text.SimpleDateFormat;
import java.util.*;

import com.example.home.mychat.provider.*;
import retrofit2.Call;
import retrofit2.Callback;

public class ChatActivity extends AppCompatActivity {

    private ChatArrayAdapter adp;
    private ListView list;
    private EditText chatText;
    private Button send;
    private boolean side = false;
    private String me;
    private String reciever;
    private HashSet<Integer> messageIds;
    private int lastSync;
    public ConnectionToServer connectionToServer = new ConnectionToServer();
    private Timer timer;
    private MyTimerTask timerTask;

    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.activity_chat );

        Object o = findViewById( R.id.toolbar_actionbar );
        android.support.v7.widget.Toolbar mActionBarToolbar = (android.support.v7.widget.Toolbar) o;
        setSupportActionBar( mActionBarToolbar );
        getSupportActionBar().setDisplayShowTitleEnabled( true );
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);


        send = (Button) findViewById( R.id.btn );

        list = (ListView) findViewById( R.id.Listview );

        adp = new ChatArrayAdapter( getApplicationContext(), R.layout.chat );

        chatText = (EditText) findViewById( R.id.chat );

        messageIds = new HashSet<>();

        //Poluchit' dannye iz ContactActivity
        Bundle b = getIntent().getExtras();
        String senderKey = b.getString( "sender" );
        String recieverKey = b.getString( "reciever" );
        me = senderKey;
        reciever = recieverKey;
        Calendar cal = Calendar.getInstance();
        cal.add( Calendar.HOUR, - 1 );
        lastSync = - 1;
        getSupportActionBar().setTitle( reciever );

        chatText.setOnKeyListener( new View.OnKeyListener() {
            @Override
            public boolean onKey( View v, int keyCode, KeyEvent event ) {

                if( ( event.getAction() == KeyEvent.ACTION_DOWN ) && ( keyCode == KeyEvent.KEYCODE_ENTER ) ) {
                    return sendChatMessage();
                }
                return false;
            }
        } );

        send.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick( View v ) {
                sendChatMessage();
            }
        } );

        list.setTranscriptMode( AbsListView.TRANSCRIPT_MODE_ALWAYS_SCROLL );
        list.setAdapter( adp );

        adp.registerDataSetObserver( new DataSetObserver() {
            public void OnChanged() {
                super.onChanged();

                list.setSelection( adp.getCount() - 1 );
            }
        } );

        timer = new Timer();
        timerTask = new MyTimerTask();
        timer.schedule( timerTask, 0, 2000 );
    }

    @Override
    public void onBackPressed() {
        Intent intent = new Intent(ChatActivity.this, ContactsActivity.class);
        Bundle b = new Bundle();
        b.putString("sender", me);
        intent.putExtras(b);
        ChatActivity.this.startActivity(intent);
        finish();
    }

    @Override
    public boolean onCreateOptionsMenu( Menu menu ) {
        getMenuInflater().inflate( R.menu.menu_chat, menu );
        return true;
    }

    @Override
    protected void onStop() {
        super.onStop();
        timer.cancel();
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();

        if (id == android.R.id.home) {
            onBackPressed();
        }

        return super.onOptionsItemSelected(item);
    }

    private boolean sendChatMessage() {
        final String newMessage = chatText.getText().toString();

        connectionToServer.getMessageService()
            .sendMessage( me, reciever, chatText.getText().toString() )
            .enqueue( new Callback<Response<MessageId>>() {
                @Override
                public void onResponse( Call<Response<MessageId>> call,
                                        retrofit2.Response<Response<MessageId>> response ) {
                    if( response.isSuccessful() ) {
                        Response<MessageId> message = response.body();
                        if( message.ErrorCode == ResponseCode.Success ) {
                            adp.add( new ChatMessage( true, newMessage ) );
                            messageIds.add( message.Data.messageNumber );
                        }
                    }else {
                        // error response, no access to resource?
                    }
                }

                @Override
                public void onFailure( Call<Response<MessageId>> call, Throwable t ) {
                }
            } );

        chatText.setText( "" );
        side = ! side;
        return true;
    }

    class MyTimerTask extends TimerTask {

        @Override
        public void run() {
            connectionToServer.getMessageService()
                .getLastMessages( me, lastSync )
                .enqueue( new Callback<Response<List<Message>>>() {
                    @Override
                    public void onResponse( Call<Response<List<Message>>> call,
                                            retrofit2.Response<Response<List<Message>>> response ) {
                        if( response.isSuccessful() ) {
                            Response<List<Message>> messages = response.body();
                            if( messages.ErrorCode == ResponseCode.Success ) {
                                Collections.sort( messages.Data, new Comparator<Message>() {
                                    @Override
                                    public int compare( Message entry1, Message entry2 ) {

                                        return Integer.compare( entry1.messageNumber, entry2.messageNumber );
                                    }
                                } );

                                for( Message message : messages.Data ){
                                    if( ! messageIds.contains( message.messageNumber ) ) {
                                        if( message.sender.equalsIgnoreCase( reciever ) ) {
                                            adp.add( new ChatMessage( false, message.text ) );
                                            messageIds.add( message.messageNumber );
                                        }else if( message.sender.equalsIgnoreCase( me )
                                            && message.reciever.equalsIgnoreCase( reciever )) {
                                            adp.add( new ChatMessage( true, message.text ) );
                                            messageIds.add( message.messageNumber );
                                        }
                                    }
                                    lastSync = Math.max( lastSync, message.messageNumber );
                                }
                            }
                        }else {
                            // error response, no access to resource?
                        }
                    }

                    @Override
                    public void onFailure( Call<Response<List<Message>>> call, Throwable t ) {
                    }
                } );
        }
    }
}
