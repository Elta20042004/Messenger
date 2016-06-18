package com.example.home.mychat;

import java.util.List;

import retrofit2.*;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

/**
 * Created by Home on 6/18/2016.
 */
public interface ContactService {
    @POST("api/contacts")
    Call<Response<Boolean>> addNewContact (@Query("userId") String userId, @Query("newContactId") String newContactId);

    @DELETE("api/contacts")
    Call<Response<Boolean>> deleteContact (@Query("userId") String userId, @Query("contactId") String contactId);

    @GET ("api/contacts")
    Call<Response<List<Message>>> getContact (@Query("userId") String userId);

}
