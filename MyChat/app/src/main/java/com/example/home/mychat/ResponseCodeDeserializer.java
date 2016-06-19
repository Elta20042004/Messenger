package com.example.home.mychat;

import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;

import java.lang.reflect.Type;

/**
 * Created by Home on 6/19/2016.
 */
public class ResponseCodeDeserializer implements
        JsonDeserializer<ResponseCode>
{
    @Override
    public ResponseCode deserialize(JsonElement json,
                                                 Type typeOfT, JsonDeserializationContext ctx)
            throws JsonParseException
    {
        int typeInt = json.getAsInt();
        return ResponseCode
                .findByAbbr(typeInt);
    }
}
