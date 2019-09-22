import { Component } from '@angular/core';
import { UserRepository } from '../model/user.repository';
import { User } from '../model/user.model';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  public users: User[];

  constructor(private repository: UserRepository, private notify: NotificationService) { }

  getUsers(): User[] {
    this.users = this.repository.getUsers();
    console.log(this.users);
    return this.users;
  }

  login() {
    this.notify.success('login');
  }
}
