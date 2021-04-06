import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './users';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http:HttpClient) { }
  async GetUsers():Promise<User[]>{
    return this.http.get<User[]>('https://reqres.in/api/users').toPromise();
  }

  async GetUser(id:number):Promise<User>{
    return this.http.get<User>('https://reqres.in/api/users/'+id).toPromise();
  }
  async PostUser():Promise<User>{
    return this.http.post<User>('https://reqres.in/api/users',{"name": "morpheus","job": "leader"}).toPromise();
  }
  async PutUser(id:number):Promise<User>{
    return this.http.post<User>('https://reqres.in/api/users'+id,{"name": "morpheus","job": "leader"}).toPromise();
  }
  async DeleteUser(id:number):Promise<User>{
    return this.http.delete<User>('https://reqres.in/api/users'+id).toPromise();
  }
}
