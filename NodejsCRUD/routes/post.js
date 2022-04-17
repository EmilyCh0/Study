const express = require('express');
const router = express.Router();
const Post = require('../models/post');

// 글 생성
router.post('/', (req, res, next) => {
    const post = new Post({
        content: req.body.content
    })
    post.save()
        .then((result) => {
            res.json(result);
        })
        .catch((error) => {
            console.error(error);
            next(error);
        })
});

// id 글 가져오기
router.get('/:id', (req, res, next) => {
    Post.find({ _id: req.params.id })
        .then((posts) => {
            res.json(posts);
        })
        .catch((error) => {
            console.error(error);
            next(error);
        })
});

// id 글 수정
router.patch('/:id', (req, res, next) => {
    Post.findOneAndUpdate({ _id: req.params.id }, { content: req.body.content })
        .then((result) => {
            res.json(result);
        })
        .catch((error) => {
            console.error(error);
            next(error);
        })
});

// id 글 삭제
router.delete('/:id', (req, res, next) => {
    Post.remove({ _id: req.params.id })
        .then((result) => {
            res.json(result);
        })
        .catch((error) => {
            console.error(error);
            next(error);
        })
});

module.exports = router;