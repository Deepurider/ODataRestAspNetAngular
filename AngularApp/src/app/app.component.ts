import { Component, OnInit } from '@angular/core';
import { ODataEntitySetService, ODataServiceFactory } from 'angular-odata';
import { Person } from './core/model/entity';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  public persons: Person[] = [];
  private personService!: ODataEntitySetService<Person>;
  constructor(private factory: ODataServiceFactory) {
    this.personService = this.factory.entitySet(Person.toString());
  }
  ngOnInit(): void {}

  private getPersons() {
    this.personService
      .entities()
      .fetch({
        withCount: true,
      })
      .subscribe({
        next: (res) => {
          if (res) {
            this.persons = res.entities ?? [];
          }
        },
        error: (arr: any) => {
          console.log(arr);
        },
      });
  }

  private createPerson() {
    const person: any = {
      PersonId: 1,
      PhoneNumber: '1010101010',
      City: 'Surat',
      Deleted: false,
      Name: 'Patel Deep',
    };
    this.personService.create(person).subscribe({
      next: (res: any) => {
        this.getPersons();
      },
    });
  }

  private updatePerson() {
    const person: any = {
      PersonId: 1,
      PhoneNumber: '1010101010',
      City: 'Surat',
      Deleted: false,
      Name: 'Patel Deep',
    };
    this.personService.update(person.PersonId, person).subscribe({
      next: (res: any) => {
        this.getPersons();
      },
    });
  }

  private deletePerson(personId: number) {
    this.personService.destroy(personId).subscribe({
      next: (res) => {
        this.getPersons();
      },
    });
  }
}
