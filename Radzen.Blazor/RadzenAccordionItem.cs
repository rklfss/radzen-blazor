﻿using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Radzen.Blazor
{
    public partial class RadzenAccordionItem : RadzenComponent
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        bool _visible = true;
        [Parameter]
        public override bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    Accordion.Refresh();
                }
            }
        }

        RadzenAccordion _accordion;

        [CascadingParameter]
        public RadzenAccordion Accordion
        {
            get
            {
                return _accordion;
            }
            set
            {
                if (_accordion != value)
                {
                    _accordion = value;
                    _accordion.AddItem(this);
                }
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.DidParameterChange(nameof(Selected), Selected))
            {
                Accordion?.SelectItem(this);
            }

            await base.SetParametersAsync(parameters);
        }

        public override void Dispose()
        {
            base.Dispose();

            Accordion?.RemoveItem(this);
        }
    }
}