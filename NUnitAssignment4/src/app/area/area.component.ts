import { Component, OnInit } from '@angular/core';
import { Area } from '../area';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {

  constructor(private area:Area) { }

  ngOnInit(): void {
  }
  Circle(r:number){
    return this.area.Circle(r);
  }
  Rectangle(l:number,b:number){
    return this.area.Rectangle(l,b);
  }
  Square(l:number){
    return this.area.Square(l);
  }
}
