import { Component, OnInit } from '@angular/core';
import { Company } from '../company.model';
import { CompanyService } from '../company.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private companyService:CompanyService) { }
  companies:Company[]=[];
  flag:boolean=false;
  ngOnInit(): void {
    this.loadScript('/assets/js/datatables-demo.js');
    this.loadScript('assets/js/scripts.js');

    this.companyService.ReceiveCompany().subscribe((data)=>{
      this.companies=data;
      this.companyService.ReceiveFlag().subscribe((data)=>{
        this.flag=data;
        if(this.flag==false){
          this.companyService.InitializeCompany();
        }
      });
    });

  }
  public delete(id:number){
    let index=this.companies.findIndex(x=>x.Id==id);
      this.companies.splice(index,1);
      this.companyService.BroadcastCompany(this.companies);
  }
  public loadScript(url: string) {
    const body = <HTMLDivElement> document.body;
    const script = document.createElement('script');
    script.innerHTML = '';
    script.src = url;
    script.async = false;
    script.defer = true;
    body.appendChild(script);
    }
}
