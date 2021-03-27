import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-string',
  templateUrl: './string.component.html',
  styleUrls: ['./string.component.css']
})
export class StringComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
  }
  lowercase(s:string){
    return s.toLowerCase();
  }
  uppercase(s:string){
    return s.toUpperCase();
  }
}
