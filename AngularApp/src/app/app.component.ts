import { Component } from '@angular/core';
import { ODataServiceFactory } from 'angular-odata';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'ODataRestWithAngular';
  constructor(private factory: ODataServiceFactory) {
    let personService = this.factory.entitySet('Person');
    let persons = personService.entities();
    // persons.fetch().subscribe({
    //   next: (res: any) => {
    //     console.log(res);
    //   },
    //   error: (arr: any) => {
    //     console.log(arr);
    //   },
    // });

    // persons.fetch({ withCount: true }).subscribe({
    //   next: (res: any) => {
    //     console.log(res);
    //   },
    //   error: (arr: any) => {
    //     console.log(arr);
    //   },
    // });

    //  persons.fetchAll().subscribe({
    //   next: (res: any) => {
    //     console.log(res);
    //   },
    //   error: (arr: any) => {
    //     console.log(arr);
    //   },
    // });

    persons
      .query((query) => query.filter().push({ PersonId: 1 }))
      .fetch()
      .subscribe({
        next: (res: any) => {
          console.log(res);
        },
      });
  }
}
