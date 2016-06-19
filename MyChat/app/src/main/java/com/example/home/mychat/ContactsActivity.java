package com.example.home.mychat;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import java.io.IOException;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;

public class ContactsActivity extends AppCompatActivity {

    private ArrayAdapter<String> adpContacts;
    private ListView listContacts;
    private EditText contactName;
    private Button addContact;
    public  ConnectionToServer connectionToServer=new ConnectionToServer();


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_contacts);

        Call<Response<List<String>>> call =
                connectionToServer.getContactService().getContacts("Lena");
        call.enqueue(new Callback<Response<List<String>>>() {
            @Override
            public void onResponse(Call<Response<List<String>>> call, retrofit2.Response<Response<List<String>>> response) {
                if (response.isSuccessful()) {
                    Response<List<String>> contacts=response.body();
                    adpContacts.addAll(contacts.Data);
                } else {
                    // error response, no access to resource?
                }
            }

            @Override
            public void onFailure(Call<Response<List<String>>> call, Throwable t) {
                String s = t.toString();
            }
        });


        addContact = (Button) findViewById(R.id.btnContacts);

        listContacts = (ListView) findViewById(R.id.ListviewContacts);

        adpContacts = new ArrayAdapter<String>(getApplicationContext(), R.layout.contact);

        contactName = (EditText) findViewById(R.id.contact);

        contactName.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {

                if ((event.getAction() == KeyEvent.ACTION_DOWN) && (keyCode == KeyEvent.KEYCODE_ENTER)) {
                    return addNewContact();
                }
                return false;
            }


        });

        addContact.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                addNewContact();
            }
        });

        listContacts.setTranscriptMode(AbsListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
        listContacts.setAdapter(adpContacts);

//        adpContacts.registerDataSetObserver(new DataSetObserver() {
//            public void OnChanged() {
//                super.onChanged();
//
//                listContacts.setSelection(adpContacts.getCount() - 1);
//            }
//        });
    }

    private boolean addNewContact() {
        final String newContact = contactName.getText().toString();

        connectionToServer.getContactService()
                .addNewContact("Lena",newContact)
                .enqueue(new Callback<Response<Boolean>>() {
            @Override
            public void onResponse(Call<Response<Boolean>> call, retrofit2.Response<Response<Boolean>> response) {
                if (response.isSuccessful()) {
                    Response<Boolean> contacts=response.body();
                    if (contacts.Data) {
                        adpContacts.add(newContact);
                    }
                } else {
                    // error response, no access to resource?
                }
            }

            @Override
            public void onFailure(Call<Response<Boolean>> call, Throwable t) {
                String s = t.toString();
            }
        });

        contactName.setText("");

        return true;
    }
}
