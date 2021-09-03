import { Component, OnInit } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserAccountService } from 'src/app/service/user-account.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  username = '';
  password = '';
  isLoading = false;
  matcher = new MyErrorStateMatcher();
  constructor(
    private userService: UserAccountService,
    private router: Router,
    private formBuilder: FormBuilder) { }
  public success: boolean = true;

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: [null, Validators.required],
      password: [null, Validators.required]
    });
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.isLoading = true;
    this.userService.login(this.f.username.value, this.f.password.value)
      .subscribe(res => {
        if (res) {
          this.isLoading = false;       
          this.router.navigate(['products']);
          this.success = true;
        } else {
          this.loginForm.reset(); 
          this.success = false;
        }
      });
  }
}