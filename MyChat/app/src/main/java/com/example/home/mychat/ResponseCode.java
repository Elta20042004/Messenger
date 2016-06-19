package com.example.home.mychat;

/**
 * Created by Home on 6/18/2016.
 */
public enum ResponseCode {
    Success(0),
    SenderNotFound(1),
    RecieverNotFound(2),
    InternalError(3);

    private final int value;
    public int getValue() {
        return value;
    }

    private ResponseCode(int value) {
        this.value = value;
    }

    public static ResponseCode findByAbbr(int value)
    {
        for (ResponseCode currEnum : ResponseCode.values())
        {
            if (currEnum.value == value)
            {
                return currEnum;
            }
        }

        return null;
    }
}
