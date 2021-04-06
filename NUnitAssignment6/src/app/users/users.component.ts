import { Component, OnInit } from '@angular/core';
import { User } from '../users';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  constructor(private userService:UsersService) { }

  ngOnInit(): void {
    this.GetUsers().then((data)=>{
      console.log(data);
    });
  }
  async GetUsers():Promise<User[]>{
    return await this.userService.GetUsers();
 }
 async GetUser(id:number):Promise<User>{
  return await this.userService.GetUser(id);
}
async PostUser():Promise<User>{
  return await this.userService.PostUser();
}
async PutUser(id:number):Promise<User>{
  return await this.userService.PutUser(id);
}
async DeleteUser(id:number):Promise<User>{
  return await this.userService.DeleteUser(id);
}
}
