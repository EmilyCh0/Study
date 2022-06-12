package com.example.fcm

import android.app.NotificationManager
import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import com.example.fcm.databinding.ActivityNotificationBinding

class NotificationActivity : AppCompatActivity() {
    lateinit var binding: ActivityNotificationBinding
    private var title = "title2"
    private var body = "body2"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityNotificationBinding.inflate(layoutInflater)
        setContentView(binding.root)


        val extra: Intent? = intent
        if(extra!=null){
            title = extra.getStringExtra("title").toString()
            body = extra.getStringExtra("body").toString()
            Log.d("tag_extra", title)
            Log.d("tag_extra", body)
        }

        binding.title.text = title
        binding.body.text = body

        val notificationManager: NotificationManager = this.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        notificationManager.cancel(0)
    }
}