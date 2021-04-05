import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AreaService {

  constructor() { }
  Circle(r:number):number{
    return 3.14*r*r;
  }
  Square(l:number):number{
    return l*l;
  }
  Rectangle(l:number,b:number):number{
    return l*b;
  }
}
