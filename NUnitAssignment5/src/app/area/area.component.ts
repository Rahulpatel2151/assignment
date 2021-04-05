import { Component, OnInit } from '@angular/core';
import { AreaService } from '../area.service';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {

  constructor(private areaService:AreaService) { }

  ngOnInit(): void {
  }
  Circle(r:number){
    return this.areaService.Circle(r);
  }
  Rectangle(l:number,b:number){
    return this.areaService.Rectangle(l,b);
  }
  Square(l:number){
    return this.areaService.Square(l);
  }
}
