import { Injectable } from '@angular/core';
import { BehaviorSubject, map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environment/environment';
import { User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject< User | null> (1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  // PersistentLogIn
  loadCurrentUser(token : string | null){
    if (token == null){
      this.currentUserSource.next(null);
      return of(null);
    }


    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);
  
    //get user
    return this.http.get<User>(this.baseUrl + 'account', { headers }).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);  // This updates the currentUserSource
          return user
        } else {
          return null;
        }
      })
    );
  }

  login(value: any){
    return this.http.post<User>(this.baseUrl + 'account/login', value).pipe(
      map (user =>{
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);  // This will update the currentUserSource
      })
    )
  }

  register(value: any){
    return this.http.post<User>(this.baseUrl + 'account/register', value).pipe(
      map (user =>{
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);  // This will update the currentUserSource
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
