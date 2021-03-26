import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyRoutingModule } from './company-routing.module';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { ViewComponent } from './view/view.component';
import { CompanyService } from './company.service';
import {MatInputModule} from '@angular/material/input';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';


@NgModule({
  declarations: [ListComponent, AddComponent, EditComponent, ViewComponent],
  imports: [
    CommonModule,
    CompanyRoutingModule,
    MatInputModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers:[CompanyService]
})
export class CompanyModule { }
