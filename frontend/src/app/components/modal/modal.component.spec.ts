import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalComponent } from './modal.component';
import { CompanyService } from 'src/app/services/company.service';
import { FormsModule } from '@angular/forms';

describe('ModalComponent', () => {
  let component: ModalComponent;
  let fixture: ComponentFixture<ModalComponent>;
  let companyServiceSpy: jasmine.SpyObj<CompanyService>;

  beforeEach(() => {
    const companySpy = jasmine.createSpyObj('CompanyService', ['GetAllCompanies']);
    const authSpy = jasmine.createSpyObj('AuthService', ['GetToken']);

    TestBed.configureTestingModule({
      declarations: [ModalComponent],
      providers: [
        { provide: CompanyService, useValue: companySpy }
      ],
      imports: [FormsModule],
    });

    fixture = TestBed.createComponent(ModalComponent);
    component = fixture.componentInstance;
    companyServiceSpy = TestBed.inject(CompanyService) as jasmine.SpyObj<CompanyService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
