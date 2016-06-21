package com.example.home.mychat;

import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import org.junit.Assert;
import org.junit.Test;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.GsonConverterFactory;
import static org.junit.Assert.*;

/**
 * To work on unit tests, switch the Test Artifact in the Build Variants view.
 */
public class ExampleUnitTest {
    @Test
    public void addition_isCorrect() throws Exception {
       ConnectionToServer connectionToServer = new ConnectionToServer();

       Response<com.example.home.mychat.Response<Boolean>> response =
               connectionToServer.getContactService().addNewContact("Lena","Alex").execute();
       Response<com.example.home.mychat.Response<MessageId>> messageResponse =
               connectionToServer.getMessageService().sendMessage("Alex","Lena","I love you").execute();

       String date = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss").format(new Date(0));

       Call<com.example.home.mychat.Response<List<Message>>> res =
               connectionToServer.getMessageService().getLastMessages("Lena",  date);
       Response<com.example.home.mychat.Response<List<Message>>> resp = res.execute();
       com.example.home.mychat.Response<List<Message>> resList = resp.body();

       Assert.assertEquals(1,resList.Data.size());
       Assert.assertEquals("I love you",resList.Data.get(0));
    }

    @Test
    public void contacts_isCorrect() throws Exception {
        ConnectionToServer connectionToServer = new ConnectionToServer();

        Response<com.example.home.mychat.Response<Boolean>> response =
                connectionToServer.getContactService().addNewContact("Lena","Alex").execute();
        Response<com.example.home.mychat.Response<List<String>>> contacts=
                connectionToServer.getContactService().getContacts("Lena").execute();
    }
}