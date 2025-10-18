import { FiCheck, FiX } from 'react-icons/fi';

interface ComparisonRow {
  feature: string;
  solaris: boolean | string;
  competitors: boolean | string;
}

const comparisonData: ComparisonRow[] = [
  {
    feature: 'Project Management Software',
    solaris: true,
    competitors: false
  },
  {
    feature: 'Real-time Monitoring',
    solaris: true,
    competitors: true
  },
  {
    feature: 'Transparent Pricing',
    solaris: true,
    competitors: false
  },
  {
    feature: '24/7 Customer Support',
    solaris: true,
    competitors: 'Limited'
  }
];

export default function ComparisonTable() {
  return (
    <div className="max-w-4xl mx-auto overflow-hidden rounded-xl border border-white/10">
      {/* Table Header */}
      <div className="grid grid-cols-3 bg-white/5">
        <div className="p-6 font-bold text-white border-r border-white/10">
          Feature
        </div>
        <div className="p-6 font-bold text-center bg-primary text-black">
          Solaris
        </div>
        <div className="p-6 font-bold text-white text-center border-l border-white/10">
          Competitors
        </div>
      </div>

      {/* Table Rows */}
      {comparisonData.map((row, index) => (
        <div
          key={index}
          className="grid grid-cols-3 border-t border-white/10 bg-white/[0.02] hover:bg-white/5 border hover:border-primary/30 transition-colors"
        >
          {/* Feature Name */}
          <div className="p-6 text-gray-300 border-r border-white/10">
            {row.feature}
          </div>

          {/* Solaris Column */}
          <div className="p-6 flex items-center justify-center bg-primary/10">
            {typeof row.solaris === 'boolean' ? (
              row.solaris ? (
                <FiCheck className="w-6 h-6 text-green-400" />
              ) : (
                <FiX className="w-6 h-6 text-red-400" />
              )
            ) : (
              <span className="text-gray-300 text-sm">{row.solaris}</span>
            )}
          </div>

          {/* Competitors Column */}
          <div className="p-6 flex items-center justify-center border-l border-white/10">
            {typeof row.competitors === 'boolean' ? (
              row.competitors ? (
                <FiCheck className="w-6 h-6 text-green-400" />
              ) : (
                <FiX className="w-6 h-6 text-red-400" />
              )
            ) : (
              <span className="text-gray-400 text-sm">{row.competitors}</span>
            )}
          </div>
        </div>
      ))}
    </div>
  );
}