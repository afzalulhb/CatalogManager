var CustomGridEditor = (function () {
    /**
    * Initializes a new instance of a CustomGridEditor.
    *
    * @param col Column that will be edited with the custom editor.
    * @param edt HTMLElement that hosts the custom editor.
    */
    function CustomGridEditor(col, edt) {
        var _this = this;
        // save references
        this._grid = col.grid;
        this._col = col;
        this._edt = edt;
        this._ctl = wijmo.Control.getControl(edt);

        // optional: increase row height a little to give editors more room
        //this._grid.rows.defaultSize = 32;
        // remove editor from DOM
        this._edt.parentElement.removeChild(this._edt);

        // connect grid events
        this._grid.beginningEdit.addHandler(this._beginningEdit, this);
        this._grid.scrollPositionChanged.addHandler(this._closeEditor, this);

        // connect editor events
        this._edt.addEventListener('keydown', function (e) {
            switch (e.keyCode) {
                case 13 /* Enter */:
                    _this._closeEditor(true);
                    _this._grid.focus();
                    break;
                case 27 /* Escape */:
                    _this._closeEditor(false);
                    _this._grid.focus();
                    break;
            }
        });
        this._ctl.lostFocus.addHandler(function () {
            if (!_this._ctl.containsFocus()) {
                _this._closeEditor(true);
            }
        });

        // close editor when user resizes the window
        window.addEventListener('resize', function () {
            if (_this._containsFocus(_this._edt)) {
                _this._closeEditor(true);
                _this._grid.focus();
            }
        });
    }
    // handle the grid's beginningEdit event by canceling the built-in editor,
    // initializing the custom editor and giving it the focus.
    CustomGridEditor.prototype._beginningEdit = function (grid, args) {
        // check that this is our column
        if (grid.columns[args.col] == this._col) {
            // cancel built-in editor
            args.cancel = true;

            // save cell being edited
            this._rng = args.cellRange;

            // initialize editor host
            var rc = grid.getCellBoundingRect(args.row, args.col);
            wijmo.setCss(this._edt, {
                position: 'absolute',
                left: rc.left - 1 + pageXOffset,
                top: rc.top - 1 + pageYOffset,
                width: rc.width + 1,
                height: grid.rows[args.row].renderHeight + 1,
                borderRadius: '0px'
            });

            // initialize editor content
            if (this._ctl != null) {
                if (!wijmo.isUndefined(this._ctl['value'])) {
                    this._ctl['value'] = grid.getCellData(this._rng.row, this._rng.col, false);
                } else if (!wijmo.isUndefined(this._ctl['text'])) {
                    this._ctl['text'] = grid.getCellData(this._rng.row, this._rng.col, true);
                } else {
                    throw 'Can\'t set editor value/text...';
                }
            }

            // start editing item
            var ecv = wijmo.tryCast(grid.collectionView, 'IEditableCollectionView'), item = grid.rows[args.row].dataItem;
            if (ecv && item) {
                setTimeout(function () {
                    ecv.editItem(item);
                }, 100);
            }

            // activate editor
            document.body.appendChild(this._edt);
            this._edt.focus();
        }
    };

    // close the custom editor, optionally saving the edits back to the grid
    CustomGridEditor.prototype._closeEditor = function (saveEdits) {
        // check that the editor is active
        var p = this._edt.parentElement, edt = wijmo.Control.getControl(this._edt);
        if (p) {
            // save editor value into grid
            if (saveEdits) {
                if (edt != null) {
                    if (!wijmo.isUndefined(edt['value'])) {
                        this._grid.setCellData(this._rng.row, this._rng.col, edt['value']);
                    } else if (!wijmo.isUndefined(edt['text'])) {
                        this._grid.setCellData(this._rng.row, this._rng.col, edt['text']);
                    } else {
                        throw 'Can\'t get editor value/text...';
                    }
                    this._grid.invalidate();
                }
            }

            // close editor and remove it from the DOM
            if (edt instanceof wijmo.input.DropDown) {
                edt.isDroppedDown = false;
            }
            p.removeChild(this._edt);
        }
    };

    // checks whether an element contains the focus
    CustomGridEditor.prototype._containsFocus = function (element) {
        var control = wijmo.Control.getControl(element);
        return control ? control.containsFocus() : wijmo.contains(element, document.activeElement);
    };
    return CustomGridEditor;
})();
//# sourceMappingURL=CustomGridEditor.js.map
