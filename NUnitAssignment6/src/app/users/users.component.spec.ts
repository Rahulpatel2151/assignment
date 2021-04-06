import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { UsersService } from '../users.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';

import { UsersComponent } from './users.component';

describe('UsersComponent', () => {
  let component: UsersComponent;
  let fixture: ComponentFixture<UsersComponent>;

  let service:UsersService;


  beforeEach(() => {

    TestBed.configureTestingModule({
      imports:[HttpClientModule],
    });
    fixture = TestBed.createComponent(UsersComponent);

    service=TestBed.inject(UsersService);
    component = new UsersComponent(service);


  });
  afterEach(() => {
    service = null;
    component = null;
  });
  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get users', (done) => {
    let users;
    component.GetUsers().then((data)=>{
      users=data;
      expect(users!=undefined).toBeTruthy();
      done();
    });
  });
  it('should get particular user', (done) => {
    let user;
    component.GetUser(1).then((data)=>{
      user=data;
      expect(user!=undefined).toBeTruthy();
      done();
    });
  });
  it('should post user', (done) => {
    let user;
    component.PostUser().then((data)=>{
      user=data;
      expect(user!=undefined).toBeTruthy();
      done();
    });
  });
  it('should put user', (done) => {
    let user;
    component.PutUser(3).then((data)=>{
      user=data;
      expect(user!=undefined).toBeTruthy();
      done();
    });
  });
  it('should delete user', (done) => {
    let user;
    component.DeleteUser(3).then((data)=>{
      user=data;
      expect(user==null).toBeTruthy();
      done();
    });
  });
});
