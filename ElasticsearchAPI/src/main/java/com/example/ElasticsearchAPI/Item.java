package com.example.ElasticsearchAPI;

import lombok.Getter;
import lombok.Setter;
import org.springframework.data.annotation.Id;
import org.springframework.data.elasticsearch.annotations.Document;

@Setter
@Getter
@Document(indexName = "items")
public class Item {
    @Id
    private String title;
    private String body;
}
