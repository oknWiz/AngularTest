import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from './model/user.model';
import { StorageService } from './service/storage.service';
import { UserAccountService } from './service/user-account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  public currentUser : User = new User();
  constructor(private userService: UserAccountService, private storageService: StorageService, private router: Router) {

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const url: string = state.url;
    return this.checkLogin(url);
  }

  checkLogin(url: string): boolean {
    this.currentUser = this.storageService.getLoginUser();
  if (Object.keys(this.currentUser).length) {
      return true;
    }
    this.userService.redirectUrl = url;
    this.router.navigate(['/login']);
    return false;
  }
}
