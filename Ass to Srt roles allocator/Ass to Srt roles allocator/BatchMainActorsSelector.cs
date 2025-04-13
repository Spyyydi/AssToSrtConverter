using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass_to_Srt_roles_allocator
{
    public partial class BatchMainActorsSelector : Form
    {
        const string RIGHT_ARROW = "→";
        const int ALLOCED_ACTORS_LABEL = 0;
        const int MAIN_ACTORS_LABEL = 1;

        public event Action<List<string>> ChangeMainActorsEvent;
        public event Action SaveMainActorsEvent;
        public event Action MainActorsChangedEvent;

        private List<string> allocatedActors;
        private List<string> lastSavedMainActors;

        public BatchMainActorsSelector(IReadOnlyList<string> import, IReadOnlyList<string> main, IReadOnlyList<string> lastSavedMain)
        {
            InitializeComponent();
            lastSavedMainActors = new List<string>();
            lastSavedMainActors.AddRange(lastSavedMain.Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                             .ToArray());

            lstMainActors.Items.AddRange(main.Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                             .Select(s => ChangeToArr(s))
                                             .ToArray());
            ModifyLabel(MAIN_ACTORS_LABEL);

            if (IsLastSavedMainActorsChanged() && lstMainActors.Items.Count > 0)
                btnSave.Enabled = true;
            else btnSave.Enabled = false;

            ModifyDeleteButton();

            allocatedActors = new List<string>();

            SetAllocedActors(import);
        }

        #region Additional methods
        public void SetAllocedActors(IReadOnlyList<string> import)
        {
            allocatedActors.Clear();
            allocatedActors.AddRange(import.Where(s => s.Last() != ':').ToArray());

            UpdateAllocatedActorsListBox();
            ModifyLabel(ALLOCED_ACTORS_LABEL);
            ModifyMoveButton();
        }

        public void SetMainActorsAfterReload(IReadOnlyList<string> main)
        {
            lastSavedMainActors.Clear();
            lastSavedMainActors.AddRange(main.Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                             .ToArray());
            lstMainActors.Items.Clear();
            lstMainActors.Items.AddRange(lastSavedMainActors.Select(s => ChangeToArr(s))
                                                            .ToArray());
            ModifyLabel(MAIN_ACTORS_LABEL);
            btnSave.Enabled = false;
            ModifyDeleteButton();
        }

        private void UpdateAllocatedActorsListBox()
        {
            string[] allocatedActorsArr = allocatedActors.Where(s => !IsMainActorsContainsActor(s.Substring(0, s.IndexOf(':'))))
                                                         .Select(s => ChangeToArr(s))
                                                         .ToArray();
            lstAllocedActors.Items.Clear();

            if (allocatedActorsArr.Length > 0)
                lstAllocedActors.Items.AddRange(allocatedActorsArr);
        }

        private bool IsMainActorsContainsActor(string actor)
        {
            return lstMainActors.Items.Cast<string>().Any(s => s.StartsWith(actor));
        }

        private string ChangeToArr(string actor)
        {
            string str = "";
            if (actor.Contains(':'))
            {
                str = actor.Substring(0, actor.IndexOf(':'));
                str += $" {RIGHT_ARROW} ";
                str += actor.Substring(actor.IndexOf(':') + 1);
            }
            return str;
        }

        private string ChangeToColon(string actor)
        {
            string str = "";
            if (actor.Contains(RIGHT_ARROW))
            {
                str = actor.Substring(0, actor.IndexOf(RIGHT_ARROW)).Trim();
                str += ":";
                str += actor.Substring(actor.IndexOf(RIGHT_ARROW) + 1).Trim();
            }
            return str;
        }

        private void ModifyLabel(int labelNum)
        {
            if (labelNum == ALLOCED_ACTORS_LABEL)
            {
                lblAllocedActors.Text = $"Allocated actors: {lstAllocedActors.Items.Count} | Selected: {lstAllocedActors.SelectedItems.Count}";
            }
            else if (labelNum == MAIN_ACTORS_LABEL)
            {
                lblMainActors.Text = $"Main actors: {lstMainActors.Items.Count} | Selected: {lstMainActors.SelectedItems.Count}";
            }
        }

        private bool IsLastSavedMainActorsChanged()
        {
            List<string> currMainActors = lstMainActors.Items.Cast<string>()
                                                             .Select(s => ChangeToColon(s))
                                                             .Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                                             .ToList();
            if (lastSavedMainActors.Count != currMainActors.Count)
                return true;

            var groupedLastSavedMainActors = lastSavedMainActors.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            var groupedCurrMainActors = currMainActors.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            return !(groupedLastSavedMainActors.Count == groupedCurrMainActors.Count &&
                   !groupedLastSavedMainActors.Except(groupedCurrMainActors).Any() &&
                   !groupedCurrMainActors.Except(groupedLastSavedMainActors).Any());
        }

        private void ModifyDeleteButton()
        {
            if (lstMainActors.Items.Count > 0 && lstMainActors.SelectedItems.Count < 1)
            {
                btnDelete.Text = "Remove all";
                btnDelete.Enabled = true;
            }
            else if (lstMainActors.Items.Count > 0 && lstMainActors.SelectedItems.Count > 0)
            {
                btnDelete.Text = "Remove";
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Text = "Remove";
                btnDelete.Enabled = false;
            }
        }
        
        private void ModifyMoveButton()
        {
            if (lstAllocedActors.SelectedItems.Count < 1)
                btnMove.Enabled = false;
            else btnMove.Enabled = true;
        }
        #endregion

        #region Button click events
        private void btnMove_Click(object sender, EventArgs e)
        {
            if (lstAllocedActors.SelectedItems.Count < 1)
            {
                btnMove.Enabled = false;
                return;
            }

            List<string> selectedItems = lstAllocedActors.SelectedItems.Cast<string>().ToList();
            foreach (var item in selectedItems)
            {
                if (!IsMainActorsContainsActor(item.Substring(0, item.IndexOf(RIGHT_ARROW)).Trim()))
                    lstMainActors.Items.Add(item);
            }
            ModifyLabel(MAIN_ACTORS_LABEL);

            UpdateAllocatedActorsListBox();
            ModifyLabel(ALLOCED_ACTORS_LABEL);

            if (IsLastSavedMainActorsChanged() && lstMainActors.Items.Count > 0)
                btnSave.Enabled = true;
            else btnSave.Enabled = false;

            ModifyDeleteButton();

            ModifyMoveButton();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lastSavedMainActors.Clear();
            lastSavedMainActors.AddRange(lstMainActors.Items.Cast<string>()
                                                             .Select(s => ChangeToColon(s))
                                                             .Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                                             .ToList());
            ChangeMainActorsEvent?.Invoke(lastSavedMainActors);
            SaveMainActorsEvent?.Invoke();
            btnSave.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> selectedItems = new List<string>();
            if (lstMainActors.SelectedItems.Count < 1)
            {
                selectedItems.AddRange(lstMainActors.Items.Cast<string>());
            }
            else
            {
                selectedItems.AddRange(lstMainActors.SelectedItems.Cast<string>());
            }

            foreach (var item in selectedItems)
            {
                lstMainActors.Items.Remove(item);
            }


            UpdateAllocatedActorsListBox();
            ModifyLabel(ALLOCED_ACTORS_LABEL);

            if (IsLastSavedMainActorsChanged() && lstMainActors.Items.Count > 0)
                btnSave.Enabled = true;
            else btnSave.Enabled = false;

            ModifyDeleteButton();
            ModifyMoveButton();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ChangeMainActorsEvent?.Invoke(lstMainActors.Items.Cast<string>()
                                                             .Select(s => ChangeToColon(s))
                                                             .Where(s => s.Count(c => c == ':') == 1 && !s.EndsWith(":") && !s.StartsWith(":"))
                                                             .ToList());

            MainActorsChangedEvent?.Invoke();

            this.Close();
        }
        #endregion

        #region Events to change buttons
        private void lstMainActors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyLabel(MAIN_ACTORS_LABEL);

            ModifyDeleteButton();
        }

        private void lstAllocedActors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyLabel(ALLOCED_ACTORS_LABEL);

            ModifyMoveButton();
        }
        #endregion
    }
}
