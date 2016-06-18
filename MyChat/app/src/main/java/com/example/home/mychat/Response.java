package com.example.home.mychat;

/**
 * Created by Home on 6/18/2016.
 */
public class Response<T>
{
    public T Data;

    public ResponseCode ErrorCode;

    public Response(T data, ResponseCode errorCode)
    {
        Data = data;
        ErrorCode = errorCode;
    }

    public Response()
    {
    }
}