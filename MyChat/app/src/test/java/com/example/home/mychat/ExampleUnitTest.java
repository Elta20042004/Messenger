package com.example.home.mychat;

import com.example.home.mychat.provider.ConnectionToServer;
import com.example.home.mychat.provider.Message;
import com.example.home.mychat.provider.MessageId;

import org.junit.Assert;
import org.junit.Test;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Response;

/**
 * To work on unit tests, switch the Test Artifact in the Build Variants view.
 */
public class ExampleUnitTest {
    @Test
    public void addition_isCorrect() throws Exception {
       ConnectionToServer connectionToServer = new ConnectionToServer();

       Response<com.example.home.mychat.provider.Response<Boolean>> response =
               connectionToServer.getContactService().addNewContact("Lena","Alex").execute();
       Response<com.example.home.mychat.provider.Response<MessageId>> messageResponse =
               connectionToServer.getMessageService().sendMessage("Alex","Lena","I love you").execute();

       Call<com.example.home.mychat.provider.Response<List<Message>>> res =
               connectionToServer.getMessageService().getLastMessages("Lena",  -1);
       Response<com.example.home.mychat.provider.Response<List<Message>>> resp = res.execute();
       com.example.home.mychat.provider.Response<List<Message>> resList = resp.body();

       Assert.assertEquals(1,resList.Data.size());
       Assert.assertEquals("I love you",resList.Data.get(0));
    }

    @Test
    public void contacts_isCorrect() throws Exception {
        ConnectionToServer connectionToServer = new ConnectionToServer();

        Response<com.example.home.mychat.provider.Response<Boolean>> response =
                connectionToServer.getContactService().addNewContact("Lena","Alex").execute();
        Response<com.example.home.mychat.provider.Response<List<String>>> contacts=
                connectionToServer.getContactService().getContacts("Lena").execute();
    }
}