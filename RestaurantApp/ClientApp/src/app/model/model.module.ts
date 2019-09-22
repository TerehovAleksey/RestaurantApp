import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RestDatasource } from './rest.datasource';
import { UserRepository } from './user.repository';

@NgModule({
  imports: [HttpClientModule],
  providers: [RestDatasource, UserRepository]
})
export class ModelModule { }
