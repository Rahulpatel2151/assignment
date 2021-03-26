import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';

const routes: Routes = [
  {path:'',redirectTo:'list',pathMatch:'full'},
  {path: 'list', component: ListComponent},
  {path: 'add', component: AddComponent},
  {path: 'edit/:id', component: EditComponent},

  {path: 'view/:id', component: ViewComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyRoutingModule { }
