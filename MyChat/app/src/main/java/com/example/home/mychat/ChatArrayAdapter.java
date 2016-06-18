package com.example.home.mychat;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Home on 6/18/2016.
 */
public class ChatArrayAdapter extends ArrayAdapter<ChatMessage>{
    private TextView chatText;
    private List<ChatMessage> messageList = new ArrayList<ChatMessage>();
    private LinearLayout layout;

    public ChatArrayAdapter(Context context, int textViewResourceId) {
        super(context, textViewResourceId);
    }

    public void add(ChatMessage chatMessage) {
        messageList.add(chatMessage);
        super.add(chatMessage);
    }

    public int getCount()
    {
        return messageList.size();
    }

    public ChatMessage getItem(int index)
    {
        return messageList.get(index);
    }

    public View getView(int position, View convertView, ViewGroup parent)
    {
        View v = convertView;
        if (v==null)
        {
            LayoutInflater inflater =
                    (LayoutInflater) getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);

            v = inflater.inflate(R.layout.chat, parent, false);

        }

        layout = (LinearLayout)v.findViewById(R.id.Message1);
        ChatMessage message = getItem(position);
        chatText = (TextView)v.findViewById(R.id.SingleMessage);

        chatText.setText(message.getText());
        chatText.setBackgroundColor(message.getSide()?Color.GREEN:Color.BLUE);

        layout.setGravity(message.getSide()? Gravity.LEFT:Gravity.RIGHT);


        return v;
    }

}
