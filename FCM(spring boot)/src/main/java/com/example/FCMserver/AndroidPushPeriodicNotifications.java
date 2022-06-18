package com.example.FCMserver;

import org.json.JSONException;
import org.json.JSONObject;


public class AndroidPushPeriodicNotifications {

    public static String PeriodicNotificationJson(String title, String content) throws JSONException {

        String device_token = "client device token";

        JSONObject body = new JSONObject();

        body.put("to", device_token);

        JSONObject notification = new JSONObject();
        // notification
        notification.put("title",title);
        notification.put("body", content);

        JSONObject data = new JSONObject();
        // data
        data.put("title", title);
        data.put("body", content);
        body.put("notification", notification);
        body.put("data", data);

        System.out.println(body.toString());

        return body.toString();
    }
}
