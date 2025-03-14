import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { environment } from 'src/environments/environment.development';

import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get token from API', () => {
    const mockToken = { token: 'mock-jwt-token'};

    service.GetToken().subscribe(response => {
      expect(response).toEqual(mockToken);
    });

    const req = httpMock.expectOne(`${environment.ApiUrl}/auth/gettoken`);
    expect(req.request.method).toBe('GET');
    req.flush(mockToken);
  });

});
