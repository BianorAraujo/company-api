import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { environment } from 'src/environments/environment.development';

import { CompanyService } from './company.service';
import { Company } from '../models/company';

describe('CompanyService', () => {
  let service: CompanyService;
  let httpMock: HttpTestingController;

  const companyListFake: Company[] = [
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
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CompanyService]
    });
    
    service = TestBed.inject(CompanyService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all companies', (done) => {
    service.GetAllCompanies().subscribe(response => {
      expect(response).toEqual(companyListFake);
      expect(response.length).toBe(2);
      done();
    });

    const req = httpMock.expectOne(`${environment.ApiUrl}/company/getall`);
    expect(req.request.method).toBe('GET');
    req.flush(companyListFake);
  });

  it('should get a company by id', (done) => {
    const companyId = 2;
    const expectedCompany = companyListFake.find(c => c.id === companyId);

    service.GetCompanyById(companyId).subscribe(response => {
      if (expectedCompany) {
        expect(response).toEqual(expectedCompany);
        expect(response?.id).toBe(companyId);
        expect(response?.name).toBe(expectedCompany?.name);
      }
      done();
    });

    const req = httpMock.expectOne(`${environment.ApiUrl}/company/getbyid/${companyId}`);
    expect(req.request.method).toBe('GET');
    if(expectedCompany){
      req.flush(expectedCompany);
    }
  });

  it('should get a company by isin', (done) => {
    const companyIsin = "IS1234567890";
    const expectedCompany = companyListFake.find(c => c.isin === companyIsin);

    service.GetCompanyByIsin(companyIsin).subscribe(response => {
      if (expectedCompany) {
        expect(response).toEqual(expectedCompany);
        expect(response?.isin).toBe(companyIsin);
        expect(response?.name).toBe(expectedCompany?.name);
      }
      done();
    });

    const req = httpMock.expectOne(`${environment.ApiUrl}/company/getbyisin/${companyIsin}`);
    expect(req.request.method).toBe('GET');
    if(expectedCompany){
      req.flush(expectedCompany);
    }
  });

  it('should add a new company', (done) => {
    const newCompany: Company = {
      "id": 3,
      "name": "Company 3",
      "exchange": "EXCH",
      "ticker": 'COMP3',
      "isin": "IS1234567892",
      "website": "http://www.company3.com"
    };

    const expextedId = 3;

    service.CreateCompany(newCompany).subscribe(response => {
        expect(response).toEqual(expextedId);
        expect(response).toBeGreaterThan(0);
        expect(response).toBeLessThan(4);
        done();
    });

    const req = httpMock.expectOne(`${environment.ApiUrl}/company/create`);
      expect(req.request.method).toBe('POST');
      req.flush(expextedId);
  });

  it('should update a company', (done) => {
    const updatedCompany: Company = {
      "id": 3,
      "name": "Updated 3",
      "exchange": "EXCH",
      "ticker": 'COMP3',
      "isin": "IS1234567892",
      "website": "http://www.company3.com"
    };

    service.UpdateCompany(updatedCompany).subscribe(response => {
        expect(response).toEqual(updatedCompany);        
        expect(response.id).toEqual(updatedCompany.id);
        done();
    });

    const req = httpMock.expectOne(`${environment.ApiUrl}/company/update/${updatedCompany.id}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedCompany);
  });

});
