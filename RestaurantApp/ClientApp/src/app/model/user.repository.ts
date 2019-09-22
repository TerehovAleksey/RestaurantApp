import { Injectable } from '@angular/core';
import { User } from './user.model';
import { RestDatasource } from './rest.datasource';

@Injectable()
export class UserRepository {
  private users: User[];

  constructor(private datasource: RestDatasource) {
    datasource.getUsers().subscribe(data => {
      this.users = data;
    });
  }

  getUsers() {
    return this.users;
  }
}
