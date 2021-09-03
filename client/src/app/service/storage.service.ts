import { Injectable } from '@angular/core';
import { User } from '../model/user.model';

const LOGIN_USER = 'login-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  saveLoginUser(login: User) {
    localStorage.setItem(LOGIN_USER, JSON.stringify(login));
  }

  getLoginUser(): User {
    return JSON.parse(localStorage.getItem(LOGIN_USER) || '{}');

  }

  removeLoginUser(): void {
    localStorage.removeItem(LOGIN_USER);
  }
}
