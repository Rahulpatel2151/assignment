import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  user:User=new User();
  constructor() { }
  RequiredValidationName= new FormControl('',[
    Validators.required
  ]);
  RequiredValidationEmail= new FormControl('',[
    Validators.required,
    Validators.email
  ]);
  userForm: FormGroup = new FormGroup({
    Name: this.RequiredValidationName,
    Email:this.RequiredValidationEmail    
  });
  ngOnInit(): void {
  }
  OnSubmit(){

  }
  
}
export class User{
  Name!:string;
  Email!:string;
}
