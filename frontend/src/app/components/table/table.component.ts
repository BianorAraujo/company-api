import { Component } from '@angular/core';
import { Company } from 'src/app/models/company';
import { CompanyService } from 'src/app/services/company.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {

  companies: Company[] = [];
  searchId: number | null = null;
  searchIsin: string = '';
  searchIdError: string = '';
  searchIsinError: string = '';
  selectedSearch: string = 'id';

  constructor(private companyService: CompanyService) {}

  ngOnInit(): void {
    this.getAllCompanies();
  }

  getAllCompanies(): void {
    this.companyService.GetAllCompanies().subscribe(data => {
      this.companies = data;
    });
  }

  searchById(): void {
    if (this.searchId === null || this.searchId === undefined || this.searchId.toString().trim() === '') {
      this.searchIdError = "Please enter a valid ID";
    } else {
      this.searchIdError = "";

      this.companyService.GetCompanyById(this.searchId).subscribe({
        next: (data) => {
          this.companies = [data];
        },
        error: (error) => {
          if (error.status === 404) {
            this.searchIdError = "Company not found";
            this.companies = [];
          } else {
              this.searchIdError = "An error occurred while searching";
          }
        }
      });
    }
  }

  searchByIsin(): void {
    if (this.searchIsin === null || this.searchIsin === undefined || this.searchIsin.toString().trim() === '') {
      this.searchIsinError = "Please enter a valid ISIN";
    } else {
      this.searchIsinError = "";

      this.companyService.GetCompanyByIsin(this.searchIsin).subscribe({
        next: (data) => {
          this.companies = [data];
        },
        error: (error) => {
          if (error.status === 404) {
            this.searchIsinError = "Company not found";
            this.companies = [];
          } else {
              this.searchIsinError = "An error occurred while searching";
          }
        }
      });
    }
  }

  clearSearch(): void {
    this.searchId = null;
    this.searchIsin = '';
    this.searchIdError = '';
    this.searchIsinError = '';
    this.selectedSearch = 'id';
    this.getAllCompanies();
  }

  clearFiels(): void {
    this.clearSearch();
  }

  preventInvalidInput(event: KeyboardEvent) {
    const invalidChars = ["e", "E", "+", "-"];
    if (invalidChars.includes(event.key)) {
      event.preventDefault();
    }
  }
  
  preventPaste(event: ClipboardEvent) {
    const clipboardData = event.clipboardData?.getData("text") || "";
    if (!/^\d+$/.test(clipboardData)) {
      event.preventDefault();
    }
  }

  //Modal
  selectedItem: any = null;
  showModal: boolean = false;
  isNew: boolean = false;

  openAddModal() {
    this.showModal = true;
    this.isNew = true;
    this.selectedItem = { 
      id: 0, 
      name: '',
      exchange: '',
      ticker: '',
      isin: '',
      website: '',
    };
  }

  openEditModal(item: any) {
    this.showModal = true;
    this.isNew = false;
    this.selectedItem = { ...item };
  }

  saveChanges(updatedItem: any) {
    if (this.isNew) {
      this.companies.push(updatedItem);
    }
    else {
      const index = this.companies.findIndex(i => i.id === updatedItem.id);

      if (index !== -1) {
        this.companies[index] = updatedItem;
      }
    }
    
    //this.showModal = false;
  }

  closeModal() {
    this.selectedItem = null;
    this.showModal = false;
  }
}
