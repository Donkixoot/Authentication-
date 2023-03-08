import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post.list.component.css']
})
export class PostListComponent implements OnInit {
  user!: User;
  posts!: Post[];

  constructor(private router: Router, private authService: AuthService,
    private postService: PostService) {}

  ngOnInit(): void {
    let user = this.authService.getCurrentUser();
    if (user) {
      this.user = user;
      this.postService.getAllPosts(user.email).subscribe(posts => {
        this.posts = posts;
      });
    }
  }

  public deletePost(post: Post): void {
    this.postService.deletePost(post.id).subscribe(isDeleted => {
      if (isDeleted) {
        this.posts.splice(this.posts.indexOf(post), 1);
      }
    });
  }

  createPost(): void {
    this.router.navigate(['/post/0']);
  }

  updatePost(post: Post): void {
    this.router.navigate([`/post/${post.id}`]);
  }
}
