package com.example.home.mychat.provider;

import retrofit2.Call;
import retrofit2.http.POST;
import retrofit2.http.Query;

/**
 * Created by Home on 7/9/2016.
 */
public interface LoginService {

    @POST("api/login")
    Call<Response<Boolean>> LoginPasswordVerification( @Query("login") String login,
                                                       @Query("password") String password);
}
