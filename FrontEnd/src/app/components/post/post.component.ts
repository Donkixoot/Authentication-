import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { PostRequest } from 'src/app/models/post.request';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
})
export class PostComponent implements OnInit {
  public postId!: number;
  postForm!: FormGroup;

  constructor(
    private _route: ActivatedRoute,
    private _authService: AuthService,
    private _postService: PostService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    let route = this._route.snapshot.paramMap.get('id');
    if (route) {
      this.postId = +route;
      this.createForm();
      if (this.postId != 0) {
        this._postService.getPost(this.postId ).subscribe((post) => {
          this.postForm.controls['title'].setValue(post.title);
          this.postForm.controls['content'].setValue(post.content);
        });
      }
    }
  }

  createForm() {
    this.postForm = new FormGroup({
      title: new FormControl(null),
      content: new FormControl(null),
    });
  }

  onSubmit(formData: FormGroup) {
    let user = this._authService.getCurrentUser();
    if (user) {
      let post: PostRequest = {
        id: this.postId,
        title: formData.value.title,
        content: formData.value.content,
        userEmail: user.email,
      };
      this._postService.addOrUpdatePost(post).subscribe(() => {
        this._router.navigate(['/postlist']);
      });
    }
  }

  public cancel(): void {
    this._router.navigate(['/postlist']);
  }

  public isAuthenticated(): boolean {
    return this._authService.isAuthenticated();
  }
}
