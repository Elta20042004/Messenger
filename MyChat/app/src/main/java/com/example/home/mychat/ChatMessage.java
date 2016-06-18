package com.example.home.mychat;

import android.text.Editable;

/**
 * Created by Home on 6/18/2016.
 */
public class ChatMessage {
    private boolean side;
    private String text;

    public ChatMessage(boolean side, String text) {
        this.side = side;
        this.text = text;
    }

    public String getText() {
        return text;
    }

    public boolean getSide() {
        return side;
    }
}
