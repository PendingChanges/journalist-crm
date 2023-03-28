import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Pitch } from 'src/models/Pitch';

@Component({
  selector: 'app-pitch-list',
  templateUrl: './pitch-list.component.html',
  styleUrls: ['./pitch-list.component.scss'],
})
export class PitchListComponent implements OnInit {
  @Input() public showClient: boolean = false;
  @Input() public showIdea: boolean = false;

  @Input() public pitches: Pitch[] | null = [];

  constructor(private router: Router) {}
  ngOnInit(): void {}

  public onRowClick(pitch: Pitch) {
    this.router.navigate(['/pitches', pitch.id]);
  }
}
