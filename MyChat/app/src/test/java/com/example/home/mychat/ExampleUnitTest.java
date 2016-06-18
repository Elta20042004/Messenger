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
        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .setFieldNamingPolicy(FieldNamingPolicy.UPPER_CAMEL_CASE)
                .create();


        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("http://localhost:9000/")
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        MessageService messageService = retrofit.create(MessageService.class);
        ContactService contactService = retrofit.create(ContactService.class);


        Response<com.example.home.mychat.Response<Boolean>> response =
                contactService.addNewContact("Lena","Alex").execute();
        Response<com.example.home.mychat.Response<MessageId>> messageResponse =
                messageService.sendMessage("Alex","Lena","I love you").execute();

        String date = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss").format(new Date(0)).toString();

        Call<com.example.home.mychat.Response<List<Message>>> res =
                messageService.getLastMessages("Lena",  date);
        Response<com.example.home.mychat.Response<List<Message>>> resp = res.execute();
        com.example.home.mychat.Response<List<Message>> resList = resp.body();

        Assert.assertEquals(1,resList.Data.size());
        Assert.assertEquals("I love you",resList.Data.get(0));
    }
}