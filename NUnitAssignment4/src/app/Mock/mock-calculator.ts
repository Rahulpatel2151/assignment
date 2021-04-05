export class MockCalculator {
  Add(a:number,b:number){
    console.log("Mock");
    return a+b;
  }
  Substract(a:number,b:number){
    console.log("Mock");
    return a-b;
  }
  Multiply(a:number,b:number){
    console.log("Mock");
    return a*b;
  }
  Divide(a:number,b:number){
    console.log("Mock");
    return a/b;
  }
}
