import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environment/environment';
import { User } from '../shared/models/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject< User | null> (null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  login(value: any){
    return this.http.post<User>(this.baseUrl + 'account/login', value).pipe(
      map (user =>{
        localStorage.setItem('token', user.token);
        this.currentUserSource
      })
    )

  }

  register(value: any){
    return this.http.post<User>(this.baseUrl + 'account/register', value).pipe(
      map (user =>{
        localStorage.setItem('token', user.token);
        this.currentUserSource
      })
    )   
  }

  logout (){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/')
  }

  checkEmailExist(email:string){
    return this.http.get<boolean>(this.baseUrl + 'account/emailExists?email=' + email);
    

  }



}
