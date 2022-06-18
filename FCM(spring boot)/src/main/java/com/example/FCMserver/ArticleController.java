package com.example.FCMserver;

import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;

@Slf4j
@Controller
public class ArticleController {
    @PostMapping("/articles")
    public String create(ArticleForm form) {
        log.info(form.toString());
        return "articles";
    }

    @GetMapping("/")
    public String index() {
        return "index";
    }
}
