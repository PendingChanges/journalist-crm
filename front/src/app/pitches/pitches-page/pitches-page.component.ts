import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Pitch } from 'src/models/Pitch';
import { PitchesService } from 'src/services/PitchesService';

@Component({
  selector: 'app-pitches-page',
  templateUrl: './pitches-page.component.html',
  styleUrls: ['./pitches-page.component.scss'],
})
export class PitchesPageComponent implements OnInit{
  public pitches$?: Observable<Pitch[]>;

  constructor(private _pitchesService: PitchesService) {}

  ngOnInit(): void {
    this.pitches$ = this._pitchesService.pitches$;
  }
}
