package com.example.home.mychat.provider;

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
    Call<com.example.home.mychat.provider.Response<Boolean>> addNewContact ( @Query("userId") String userId, @Query("newContactId") String newContactId);

    @DELETE("api/contacts")
    Call<com.example.home.mychat.provider.Response<Boolean>> deleteContact ( @Query("userId") String userId, @Query("contactId") String contactId);

    @GET ("api/contacts")
    Call<com.example.home.mychat.provider.Response<List<String>>> getContacts ( @Query("userId") String userId);

}
