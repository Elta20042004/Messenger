package com.example.home.mychat.provider;

import com.example.home.mychat.provider.ContactService;
import com.example.home.mychat.provider.MessageService;
import com.example.home.mychat.provider.ResponseCode;
import com.example.home.mychat.provider.ResponseCodeDeserializer;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.GsonConverterFactory;
import retrofit2.Retrofit;

/**
 * Created by Home on 6/19/2016.
 */
public class ConnectionToServer {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
            .setFieldNamingPolicy(FieldNamingPolicy.UPPER_CAMEL_CASE)
            .registerTypeAdapter(ResponseCode.class, new ResponseCodeDeserializer())
            .create();

    Retrofit retrofit = new Retrofit.Builder()
            .baseUrl("http://192.168.1.100:9000/")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build();

    MessageService messageService = retrofit.create(MessageService.class);
    ContactService contactService = retrofit.create(ContactService.class);
    LoginService loginService = retrofit.create( LoginService.class );

    public MessageService getMessageService()
    {
        return messageService;
    }

    public ContactService getContactService()
    {
        return contactService;
    }

    public LoginService getLoginService() { return loginService; }
}
