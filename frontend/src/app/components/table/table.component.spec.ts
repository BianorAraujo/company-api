import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableComponent } from './table.component';
import { CompanyService } from 'src/app/services/company.service';
import { AuthService } from 'src/app/services/auth.service';
import { Company } from 'src/app/models/company';
import { of } from 'rxjs';
import { FormsModule } from '@angular/forms';

describe('TableComponent', () => {
  let component: TableComponent;
  let fixture: ComponentFixture<TableComponent>;
  let companyServiceSpy: jasmine.SpyObj<CompanyService>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;

  const mockCompanies: Company[] = [
    {
      "id": 1,
      "name": "Company 1",
      "exchange": "EXCH",
      "ticker": 'COMP1',
      "isin": "IS1234567890",
      "website": "http://www.company1.com"
    },
    {
      "id": 2,
      "name": "Company 2",
      "exchange": "EXCH",
      "ticker": 'COMP2',
      "isin": "IS1234567891",
      "website": "http://www.company2.com"
    }
  ];

  beforeEach(() => {
    const companySpy = jasmine.createSpyObj('CompanyService', ['GetAllCompanies']);
    const authSpy = jasmine.createSpyObj('AuthService', ['GetToken']);

    TestBed.configureTestingModule({
      declarations: [TableComponent],
      providers: [
        { provide: CompanyService, useValue: companySpy },
        { provide: AuthService, useValue: authSpy },
      ],
      imports: [FormsModule],
    });

    fixture = TestBed.createComponent(TableComponent);
    component = fixture.componentInstance;
    companyServiceSpy = TestBed.inject(CompanyService) as jasmine.SpyObj<CompanyService>;
    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getAllCompanies on ngOnInit', () => {
    
    companyServiceSpy.GetAllCompanies.and.returnValue(of(mockCompanies));
    authServiceSpy.GetToken.and.returnValue(of({token: 'mockToken'}));

    fixture.detectChanges();

    expect(companyServiceSpy.GetAllCompanies).toHaveBeenCalled();
    expect(component.companies).toEqual(mockCompanies);
  });

  it('should set token on ngOnInit', () => {
    authServiceSpy.GetToken.and.returnValue(of({token: 'mockToken'}));
    companyServiceSpy.GetAllCompanies.and.returnValue(of([]));
    fixture.detectChanges();
    expect(localStorage.getItem('token')).toBe('mockToken');
  });

  it('should call getAllCompanies and update companies array', () => {
    
    companyServiceSpy.GetAllCompanies.and.returnValue(of(mockCompanies));
    component.getAllCompanies();

    expect(companyServiceSpy.GetAllCompanies).toHaveBeenCalled();
    expect(component.companies).toEqual(mockCompanies);
  });
});
