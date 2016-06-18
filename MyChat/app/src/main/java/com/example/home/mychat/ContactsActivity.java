package com.example.home.mychat;

import android.database.DataSetObserver;
import android.net.Uri;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import com.google.android.gms.appindexing.Action;
import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.common.api.GoogleApiClient;

public class ContactsActivity extends AppCompatActivity {

    private ArrayAdapter<Contact> adpContacts;
    private ListView listContacts;
    private EditText contactName;
    private Button addContact;
    private boolean side = false;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.contacts);

        addContact = (Button) findViewById(R.id.btnContacts);

        listContacts = (ListView) findViewById(R.id.ListviewContacts);

        adpContacts = new ArrayAdapter<Contact>(getApplicationContext(), R.layout.contacts);

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

        adpContacts.registerDataSetObserver(new DataSetObserver() {
            public void OnChanged() {
                super.onChanged();

                listContacts.setSelection(adpContacts.getCount() - 1);
            }
        });

    }

    private boolean addNewContact() {

        adpContacts.add(new Contact(contactName.getText().toString()));
        contactName.setText("");
        return true;
    }


}
