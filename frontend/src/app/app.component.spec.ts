import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Component, Input } from '@angular/core';
import { By } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-table',
  template: '',
})
class TableStubComponent {
  @Input() companies!: any[];
}

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent, TableStubComponent],
      imports: [RouterModule.forRoot([])],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it(`should have as title 'CompanyApp'`, () => {
    expect(component.title).toEqual('CompanyApp');
  });

  it('should contain the app-table component', () => {
    const tableElement = fixture.debugElement.query(By.css('app-table'));
    expect(tableElement).toBeTruthy();
  });
});
