export class Company{
  Id:number;
  Email:string;
  Name:string;
  TotalEmployee:number;
  Address:string;
  IsCompanyActive:boolean;
  TotalBranch:number;
  CompanyBranch:Branch[];
}
export class Branch{
  BranchId:number;
  BranchName:string;
  Address:string;
}
