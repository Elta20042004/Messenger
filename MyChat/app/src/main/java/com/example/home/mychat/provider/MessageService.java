package com.example.home.mychat.provider;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

/**
 * Created by Home on 6/18/2016.
 */
public interface MessageService {
    @GET("api/message")
    Call<Response<List<Message>>> getLastMessages(@Query("userId") String sender, @Query("lastSync") String lastSync);

    @POST("api/message")
    Call<Response<MessageId>> sendMessage(@Query("sender") String sender,
                         @Query("reciever") String reciever,
                         @Query("text") String text);
}