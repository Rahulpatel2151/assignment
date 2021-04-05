import { Component, OnInit } from '@angular/core';
import { Calculator } from '../calculator';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  constructor(private calculator:Calculator) { }

  ngOnInit(): void {
  }
  Add(a:number,b:number){
    return this.calculator.Add(a,b);
  }
  Substract(a:number,b:number){
    return this.calculator.Substract(a,b);
  }
  Multiply(a:number,b:number){
    return this.calculator.Multiply(a,b);
  }
  Divide(a:number,b:number){
    return this.calculator.Divide(a,b);
  }
}
